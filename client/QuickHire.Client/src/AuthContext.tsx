import React, { createContext, useContext, useState, useEffect } from "react";
import axios from "./axiosInstance";
import { useNavigate } from "react-router-dom";
import * as signalR from "@microsoft/signalr";

type User = {
  id: string;
  email: string;
  roles: string[];
  mode: "buyer" | "seller";
  profilePictureUrl: string;
};

type AuthContextType = {
  user: User | null;
  loading: boolean;
  login: (email: string, password: string) => Promise<void>;
  logout: () => void;
  switchMode: (mode: "buyer" | "seller") => void;
  signalRConnection: signalR.HubConnection | null;
};

const AuthContext = createContext<AuthContextType | null>(null);

export const useAuth = () => useContext(AuthContext)!;

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState(true);
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
  const navigate = useNavigate();

  const startSignalRConnection = async () => {
  if (connection?.state === signalR.HubConnectionState.Connected || connection?.state === signalR.HubConnectionState.Connecting) {
    return;
  }

  try {
    const conn = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:7267/hubs/chat", {
        withCredentials: true
      })
      .withAutomaticReconnect()
      .build();

    conn.on("ReceiveMessage", (message) => {
      console.log("Received message:", message);
    });

    await conn.start();
    console.log("SignalR connected");
    setConnection(conn);
  } catch (error) {
    console.error("SignalR connection error:", error);
  }
};


  const stopSignalRConnection = async () => {
    if (connection) {
      await connection.stop();
      setConnection(null);
      console.log("SignalR disconnected");
    }
  };

  const fetchUser = async () => {
    try {
      const url = "https://localhost:7267/auth/me";
      const res = await axios.get(url);
      setUser(res.data);
      await startSignalRConnection(); 
    } catch {
      setUser(null);
      await stopSignalRConnection();
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
  if (window.location.pathname !== "/login") {
    fetchUser();
  } else {
    setLoading(false);
  }

  return () => {
    stopSignalRConnection();
  };
}, []);


  const login = async (email: string, password: string) => {
    const url = "https://localhost:7267/auth/login";
    await axios.post(url, { emailOrUsername: email, password }, { withCredentials: true });
    await fetchUser(); 
    navigate("/buyer");
  };

  const logout = async () => {
    const url = "https://localhost:7267/auth/logout";
    await axios.post(url, {}, { withCredentials: true });
    setUser(null);
    await stopSignalRConnection();
    navigate("/login");
  };

  const switchMode = async (mode: "buyer" | "seller") => {
    const url = "https://localhost:7267/auth/switch-mode";
    await axios.post(url, { mode }, { withCredentials: true });
    await fetchUser();
  };

  return (
    <AuthContext.Provider
      value={{
        user,
        loading,
        login,
        logout,
        switchMode,
        signalRConnection: connection,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};
