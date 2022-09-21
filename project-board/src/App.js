import NavigationBar from "./components/NavigationBar";
import 'bootstrap/dist/css/bootstrap.min.css';
import {BrowserRouter as Router, Routes, Route } from "react-router-dom";
import './App.css'
import Home from "./components/Home";
import BoardGameCreate from "./components/boardgame/BoardGameCreate";
import BoardGamePage from "./components/boardgame/BoardGamePage";
import Register from "./components/user/Register";
import UserProfile from "./components/user/UserProfile";
import ListingList from "./components/listing/ListingList";
import ListingEdit from "./components/listing/ListingEdit";
import OrderCreate from "./components/order/OrderCreate";
import OrderEdit from "./components/order/OrderEdit";
import UserList from './components/user/UserList'
import Groups from "./components/Groups/Groups";
import Group from "./components/Groups/Group";

function App() {
  return (
    <Router>
      <div className="App">
        <NavigationBar />
          <Routes>
            <Route path='/' element={<Home />} />
            <Route path='/marketplace' element={<ListingList/>} />
            <Route path='/groups' element={<Groups />} />
            <Route path='/group/:groupId' element={<Group/>}/>
            <Route path='/newgame' element={<BoardGameCreate />} />
            <Route path='/register' element={<Register />} />
            <Route path='/boardGame/:boardGameId' element={<BoardGamePage />} />
            <Route path='/user/:userId' element={<UserProfile />} />
            <Route path='/listing/:listingId' element={<ListingEdit />} />
            <Route path='/create-order/:listingId' element={<OrderCreate />} />
            <Route path='/order/:orderId' element={<OrderEdit />} />
            <Route path='/users' element={<UserList />} />
          </Routes>
      </div>
    </Router>
  );
}
export default App;
