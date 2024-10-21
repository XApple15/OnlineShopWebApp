import { HashRouter as Router, Routes, Route } from "react-router-dom"; // Use BrowserRouter if you prefer
import NavBar from "./Components/NavBar"; // Adjust the path as necessary
import Home from "./Pages/Home";
import About from "./Pages/About";
import Login from "./Pages/Login";
import AllProducts from "./Pages/AllProducts";
import ProductDetails from "./Pages/ProductDetails";
import NotFound from "./Pages/NotFound";


function App() {
   

    return (
        <Router>
            <NavBar  />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/about" element={<About />} />
                <Route path="/allproducts" element={<AllProducts />} />
                <Route path="/login" element={<Login />} />
                <Route path="/products/:id" element={<ProductDetails />} />
                <Route path="*" element={<NotFound />} />
            </Routes>
        </Router>
    );
}

export default App;
