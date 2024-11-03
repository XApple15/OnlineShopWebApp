
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { useState, useEffect, useContext } from 'react'
import { CartContext } from '../Context/cart.jsx'
import Cart from '../Components/Cart.jsx'

//import truncateString from "../Utilities/TruncateString"; not using anymore

function AllProducts() {
    const navigate = useNavigate();
    const [products, setProducts] = useState([]);
    const [showModal, setshowModal] = useState(false);
    const { cartItems, addToCart } = useContext(CartContext)

    const toggle = () => {
        setshowModal(!showModal);
    };

    useEffect(() => {
            axios
                .get("https://localhost:7131/api/Products")
                .then((response) => {
                    setProducts(response.data);
                })
                .catch((err) => console.log(err));
        },
        []);

    const handleClick = (product) => {
        navigate(`/products/${product.id}`);
    };

    return (
        <div className="container mt-4">
            <div className="row row-cols-1 row-cols-md-3 g-4">
                {products.map(product => (
                    
                    <div  key={product.id} className="col" onClick={() => handleClick(product)}>
                        <div className="card h-100">
                            <img src={product.productImageURL} className="card-img-top" width="200" height="300" />
                            <div key={product.id} className="card-body">
                                <h5 className="card-title">{product.name}</h5>
                            </div>
                            <button className='px-4 py-2 bg-gray-800 text-white text-xs font-bold uppercase rounded hover:bg-gray-700 focus:outline-none focus:bg-gray-700'
                                onClick={() => {addToCart(product)}}>
                                Add to cart</button>
                            <div className="card-footer">
                                <small className="text-body-secondary">{product.price} Lei</small>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}


export default AllProducts;