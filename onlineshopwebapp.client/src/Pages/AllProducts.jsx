import  { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

//import truncateString from "../Utilities/TruncateString"; not using anymore

function AllProducts() {
    const navigate = useNavigate();
    const [products, setProducts] = useState([]);


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
                    
                    <div className="col" onClick={() => handleClick(product)}>
                        <div className="card h-100">
                            <img src={product.productImageURL} className="card-img-top" width="200" height="300" />
                            <div key={product.id} className="card-body">
                                <h5 className="card-title">{product.name}</h5>
                            </div>
                        
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