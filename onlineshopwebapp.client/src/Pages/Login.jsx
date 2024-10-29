import  { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { useAuth } from '../Utilities/AuthContext';
import 'bootstrap/dist/css/bootstrap.min.css';

function Login() {
    const { login } = useAuth();
    const navigate = useNavigate();
    const [userName, setUserName] = useState("user1@example.com");
    const [password, setPassword] = useState("String1!");
    const [error, setError] = useState("");

    function handleSubmit(event) {
        event.preventDefault();

        const loginPayload = {
            username: userName,
            password: password,
           
        };

        axios
            .post("https://localhost:7131/api/Auth/Login", loginPayload)
            .then((response) => {
                const token = response.data.jwtToken;
                
                localStorage.setItem("token", token);

                login(token);

                navigate("/");
            })
            .catch((err) => {
                console.error(err);
                setError("Login failed. Please check your credentials.");
            });
    }

    function handleUserNameChange(event) {
        setUserName(event.target.value);
    }

    function handlePasswordChange(event) {
        setPassword(event.target.value);
    }

    return (
        <div className="container mt-4">
            <h2 className="text-center">Login</h2>
            {error && <div className="alert alert-danger">{error}</div>}
            <div className="row justify-content-center">
                <div className="col-md-4">
                    <form onSubmit={handleSubmit}>
                        <div className="form-group">
                            <label htmlFor="exampleInputEmail1">Email address</label>
                            <input
                                type="email"
                                className="form-control form-control-sm"
                                id="exampleInputEmail1"
                                aria-describedby="emailHelp"
                                placeholder="Enter email"
                                value={userName}
                                onChange={handleUserNameChange}
                            />
                            <small id="emailHelp" className="form-text text-muted">
                                Help
                            </small>
                        </div>
                        <div className="form-group">
                            <label htmlFor="exampleInputPassword1">Password</label>
                            <input
                                type="password"
                                className="form-control form-control-sm"
                                id="exampleInputPassword1"
                                placeholder="Password"
                                value={password}
                                onChange={handlePasswordChange}
                            />
                        </div>
                        <div className="form-group form-check">
                            <input
                                type="checkbox"
                                className="form-check-input"
                                id="exampleCheck1"
                            />
                            <label className="form-check-label" htmlFor="exampleCheck1">Remember Me</label>
                        </div>
                        <div style={{ textAlign: 'center', marginTop: '16px' }}>
                            <button type="submit" className="btn btn-primary">
                                Login
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
}

export default Login;