import { NavLink, useNavigate } from "react-router-dom";
import React, { useEffect, useState, useContext } from "react";
import axios from "axios";
import { useAuth } from '../Utilities/AuthContext';
import Cart from '../Components/Cart.jsx';
import { CartContext } from '../context/cart.jsx'
import 'bootstrap/dist/css/bootstrap.min.css';
import Login from "../Pages/Login";

function NavBar() {
    const [showModal, setshowModal] = useState(false);
    const { cartItems, addToCart } = useContext(CartContext)
    
    const { user, logout } = useAuth();
    const navigate = useNavigate();


    const toggle = () => {
        setshowModal(!showModal);
    };
    const handleRedirectRegister = () => {
        navigate('/register'); 
    };
    const handleRedirectLogIn = () => {
        navigate('/login'); 
    };
    const handleRedirectAccount = () => {
        navigate('/account');
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
                        {!showModal && <button className='px-4 py-2 bg-gray-800 text-white text-xs font-bold uppercase rounded hover:bg-gray-700 focus:outline-none focus:bg-gray-700'
                            onClick={toggle}
                        >Cart ({cartItems.length})</button>}
                        {user ? (
                            <div>
                                <button onClick={handleRedirectAccount} type="button" className="btn btn-primary" style={{ marginRight: "10px" }}>Account</button>
                                <button onClick={logout} type="button" className="btn btn-primary">Logout</button>
                            </div>
                        ) : (
                            <form className="d-flex" role="login">
                                <button onClick={handleRedirectRegister} type="button" className="btn btn-primary" style={{marginRight:"10px"}}>Register</button>
                                    <button onClick={handleRedirectLogIn} type="button" className="btn btn-primary" >Log-in</button>
                            </form>
                        )}
                    </div>
                </div>
            </nav>
        </div>
    );
}

export default NavBar;