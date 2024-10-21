import { NavLink, useNavigate } from "react-router-dom";
import React, { useEffect } from "react";
import axios from "axios";
import 'bootstrap/dist/css/bootstrap.min.css';

function NavBar() {
    const [loggedIn, setLoggedIn] = React.useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem("token");
        setLoggedIn(!!token);
    }, []);

    const handleLogout = () => {
        localStorage.removeItem("token");
        setLoggedIn(false);
        delete axios.defaults.headers.common["Authorization"];
        navigate("/");
    };

    const handleRedirectRegister = () => {
        navigate('/register'); 
    };
    const handleRedirectLogIn = () => {
        navigate('/login'); 
    };

    return (
        <div>
            <nav className="navbar navbar-expand-lg bg-body-tertiary">
                <div className="container-fluid">
                    <NavLink className="navbar-brand" to="/">OnlineShop</NavLink>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                            <li className="nav-item">
                                <NavLink className="nav-link" to="/">Home</NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className="nav-link" to="/allproducts">All Products</NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className="nav-link" to="/about">About</NavLink>
                            </li>
                        </ul>
                        {loggedIn ? (
                            <div>
                                <button type="button" className="btn btn-primary">Primary</button>
                                <button onClick={handleLogout} type="button" className="btn btn-primary">Logout</button>
                            </div>
                        ) : (
                            <form className="d-flex" role="login">
                                <button onClick={handleRedirectRegister} type="button" className="btn btn-primary">Register</button>
                                <button onClick={handleRedirectLogIn} type="button" className="btn btn-primary">Log-in</button>
                            </form>
                        )}
                    </div>
                </div>
            </nav>
        </div>
    );
}

export default NavBar;