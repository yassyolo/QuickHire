import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../AuthContext";

const GoogleLoginRedirect = () => {
  const navigate = useNavigate();
  const { fetchUser } = useAuth(); 

  useEffect(() => {
    const completeGoogleLogin = async () => {
      try {
        await fetchUser(); 
        navigate("/buyer/profile"); 
      } catch (err) {
        console.error("Google login failed:", err);
        navigate("/login");
      }
    };

    completeGoogleLogin();
  }, [fetchUser, navigate]);

  return <div>Logging you in...</div>;
};

export default GoogleLoginRedirect;
