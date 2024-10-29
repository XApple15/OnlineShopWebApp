// src/utils/jwtUtils.js

export const decodeJwt = (token) => {
    try {
        const base64Url = token.split('.')[1];
        if (!base64Url) throw new Error('Invalid token: Payload not found');

        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        const jsonPayload = atob(base64);
        return JSON.parse(jsonPayload);
    } catch (error) {
        console.error('Failed to decode JWT:', error);
        return null; // Return null or handle the error as needed
    }
};

export const isTokenExpired = (token) => {
    const decodedToken = decodeJwt(token);
    if (!decodedToken || !decodedToken.exp) return true;

    const currentTime = Date.now() / 1000; // Convert to seconds
    return decodedToken.exp < currentTime; // Token is expired if the current time exceeds the expiry
};
