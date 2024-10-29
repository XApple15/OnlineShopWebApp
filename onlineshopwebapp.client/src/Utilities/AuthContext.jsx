import React, { createContext, useContext, useEffect, useState } from 'react';
import axios from 'axios'; // Ensure axios is imported
import { decodeJwt, isTokenExpired } from '../Utilities/RetrieveDataFromJWT';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (token && !isTokenExpired(token)) {
            const decoded = decodeJwt(token);
            axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            setUser(decoded);
        }
        setLoading(false);
    }, []);

    // Remove the duplicate `login` function
    const login = (token) => {
        localStorage.setItem('token', token);
        const decoded = decodeJwt(token);
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        setUser(decoded);
    };

    const logout = () => {
        localStorage.removeItem('token');
        delete axios.defaults.headers.common["Authorization"];
        setUser(null);
    };

    return (
        <AuthContext.Provider value={{ user, loading, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);
