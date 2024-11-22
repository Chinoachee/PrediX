import axios from "axios";

export const fetchReg = async () => {
    try{
        var response = await axios.post("http://localhost:5214/users");
        console.log(response);
    }catch(e){
        console.error(e);
    }
}