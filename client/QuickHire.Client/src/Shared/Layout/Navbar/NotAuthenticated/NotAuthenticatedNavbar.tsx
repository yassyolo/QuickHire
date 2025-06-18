import { Outlet, useNavigate } from "react-router-dom";
import { Logo } from "../../Logo/Logo";
import { ActionButton } from "../../../Buttons/ActionButton/ActionButton";

export function NotAuthenticatedNavbar() {
    const navigate = useNavigate();
    return (
         <div className="page-container">
        <nav className="not-auth-navbar justify-content-between d-flex flex-row justify-content-between" style={{height: "80px", width: "100vw", padding: "20px 0px", backgroundColor: "#f8f9fa"}}>
            <div style={{paddingLeft: "50px"}}>
                <Logo />
            </div>
            <div style={{paddingRight: "50px"}}>
            <ActionButton text={"Sign up"} onClick={() => navigate("/login")} className={"sign-up-button"} ariaLabel={"Sign up button"}/>
            </div>      
        </nav>              

        <main className="not-authenticated-main" style={{width: '100vw'}}> <Outlet /> </main>

        <div className="buyer-footer"> <footer><div className="footer-bottom">
                <p>© 2025 QuickHire. All rights reserved.</p>
            </div></footer></div>
    </div>
    );
}