import React, { createContext, useContext, useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import axios from 'axios';
import { decodeJwt, isTokenExpired } from './RetrieveDataFromJWT';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [token, setToken] = useState(localStorage.getItem("token") || "");
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);
    

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (token) {
            if (isTokenExpired(token)) {     
                logout();
            } else {       
                const decoded = decodeJwt(token);
                axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
                setUser(decoded);
            }
        }
        setLoading(false);
    }, []);

    const checkTokenExpiration = () => {
        const token = localStorage.getItem('token');
        if (token && isTokenExpired(token)) {
            logout(); 
        }
    };

    useEffect(() => {
        checkTokenExpiration();
        const interval = setInterval(checkTokenExpiration, 60000);// refreshing token expiration minute by minute 
        return () => clearInterval(interval);
    }, []);

    const loginAction = async (data) => {
        let statusCode = 500;
        try {
            const loginPayload = {
                username: data.username,
                password: data.password,
            };
            await   axios
                .post("https://localhost:7131/api/Auth/Login", loginPayload)
                .then((response) => {
                    const token = response.data.jwtToken;
                    localStorage.setItem("token", token);
                    setToken(token);
                    const decoded = decodeJwt(token);
                    setUser(decoded);
                    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
                    statusCode = response.status;
                })
                .catch((err) => {        
                    console.log(err);
                    statusCode = 500;
                });
        }
        catch (error){
            console.log(error);     
            statusCode = 500;
        }
        return statusCode;
    };

    const logout = () => {
        setToken("");
        localStorage.removeItem('token');
        delete axios.defaults.headers.common["Authorization"];
        setUser(null);
        navigate('/');
    };

    return (
        <AuthContext.Provider value={{ token,user, loginAction, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);
