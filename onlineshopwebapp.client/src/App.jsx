import { HashRouter as Router, Routes, Route } from "react-router-dom"; 
import NavBar from "./Components/NavBar"; 
import Home from "./Pages/Home";
import About from "./Pages/About";
import Login from "./Pages/Login";
import AllProducts from "./Pages/AllProducts";
import ProductDetails from "./Pages/ProductDetails";
import NotFound from "./Pages/NotFound";
import Register from "./Pages/Register";
import MyAccount from "./Pages/MyAccount";
import { AuthProvider } from './Utilities/AuthContext';

function App() {
    return (
        <AuthProvider>
            <Router>
                <NavBar  />
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/about" element={<About />} />
                    <Route path="/allproducts" element={<AllProducts />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/register" element={<Register />} />
                    <Route path="/products/:id" element={<ProductDetails />} />
                    <Route path="/account" element={<MyAccount />} />"
                    <Route path="*" element={<NotFound />} />
                </Routes>
            </Router>
        </AuthProvider>
    );
}

export default App;
