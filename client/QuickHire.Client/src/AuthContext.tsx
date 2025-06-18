import React, {
  createContext,
  useContext,
  useState,
  useEffect,
  useCallback,
  useRef,
} from "react";
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

export type CustomOfferPayloadModel = {
  gigTitle: string;
  gigId: number;
  offerAmount: string;
  includes: string[];
  offerId: number;
  senderUsername: string;
};

type NewMessage = {
  text: string;
  conversationId?: number;
  receiverId: string;
  payload?: CustomOfferPayloadModel | null;
  attachmentUrl?: string | null;
};

type AuthContextType = {
  user: User | null;
  loading: boolean;
  login: (email: string, password: string) => Promise<void>;
  logout: () => void;
  switchMode: (mode: "buyer" | "seller") => void;
  signalRConnection: signalR.HubConnection | null;
  sendMessage: (message: NewMessage) => Promise<void>;
  registerOnReceiveMessage: (callback: (message: unknown) => void) => void;
  fetchUser: () => Promise<void>;
};

const AuthContext = createContext<AuthContextType | null>(null);
export const useAuth = () => useContext(AuthContext)!;

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children,
}) => {
  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState(true);
  const connectionRef = useRef<signalR.HubConnection | null>(null);
  const onReceiveMessageCallbackRef = useRef<((message: unknown) => void) | null>(null);

  const navigate = useNavigate();

  const registerOnReceiveMessage = useCallback((callback: (message: unknown) => void) => {
    onReceiveMessageCallbackRef.current = callback;
  }, []);

  const startSignalRConnection = useCallback(async () => {
    if (connectionRef.current) {
      if (
        connectionRef.current.state === signalR.HubConnectionState.Connected ||
        connectionRef.current.state === signalR.HubConnectionState.Connecting
      ) {
        return; 
      }
    }

    try {
      const conn = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7267/hubs/chat", { withCredentials: true })
        .withAutomaticReconnect()
        .build();

      conn.on("ReceiveMessage", (message) => {
        console.log("📨 New message received:", message);
        onReceiveMessageCallbackRef.current?.(message);
      });

      conn.onreconnected(() => {
        console.log("🔁 SignalR reconnected");
      });

      await conn.start();
      console.log("✅ SignalR connected");
      connectionRef.current = conn;
    } catch (error) {
      console.error("❌ SignalR connection error:", error);
    }
  }, []);

  const stopSignalRConnection = useCallback(async () => {
    if (connectionRef.current) {
      try {
        await connectionRef.current.stop();
        console.log("SignalR disconnected");
      } catch (err) {
        console.error("Error disconnecting SignalR:", err);
      }
      connectionRef.current = null;
    }
  }, []);

const fetchUser = useCallback(async (): Promise<void> => {
  try {
    const res = await axios.get("https://localhost:7267/auth/me");
    setUser(res.data);
    await startSignalRConnection();
  } catch {
    setUser(null);
    await stopSignalRConnection();
  } finally {
    setLoading(false);
  }
}, [startSignalRConnection, stopSignalRConnection]);


 const login = useCallback(
  async (email: string, password: string) => {
    await axios.post(
      "https://localhost:7267/auth/login",
      { emailOrUsername: email, password },
      { withCredentials: true }
    );  
    await fetchUser();
  },
  []
);

  const logout = useCallback(async () => {
    await axios.post("https://localhost:7267/auth/logout", {}, { withCredentials: true });
    setUser(null);
    await stopSignalRConnection();
    navigate("/login");
  }, [stopSignalRConnection, navigate]);

  const switchMode = useCallback(
    async (mode: "buyer" | "seller") => {
      await axios.post(
        "https://localhost:7267/auth/switch-mode",
        { mode },
        { withCredentials: true }
      );
      await fetchUser();
    },
    [fetchUser]
  ); 
  
  type MessageType = "CustomOffer" | "Revision" | "Delivery";

 const sendMessage = useCallback(
  async (message: NewMessage & { payloadType?: MessageType }) => {
    const conn = connectionRef.current;
    if (conn?.state !== signalR.HubConnectionState.Connected) {
      console.error("SignalR not connected. Message not sent.");
      return;
    }

    try {
      const payloadJson = message.payload ? JSON.stringify(message.payload) : null;

      await conn.invoke(
        "SendMessage",
        message.text,
        message.conversationId ?? null,
        message.attachmentUrl ?? null,
        payloadJson,                    
        message.payload ? message.payloadType ?? "CustomOffer" : null, 
        message.receiverId
      );
    } catch (error) {
      console.error("Error sending message:", error);
    }
  },
  []
);


  useEffect(() => {
    if (window.location.pathname !== "/login") {
      fetchUser();
    } else {
      setLoading(false);
    }

    return () => {
      stopSignalRConnection();
    };
  }, [fetchUser, stopSignalRConnection]);

  return (
    <AuthContext.Provider
      value={{
        user,
        loading,
        login,
        logout,
        switchMode,
        signalRConnection: connectionRef.current,
        sendMessage,
        registerOnReceiveMessage,
        fetchUser,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};
