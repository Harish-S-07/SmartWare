import './App.css';
import { AuthProvider } from './context/AuthContext';
import { Route, BrowserRouter as Router, Routes } from 'react-router-dom';
import Login from './pages/Login.jsx';
import HomePage from './pages/HomePage.jsx';
import PrivateRoute from './components/PrivateRoute';

function App() {
  return (
    <AuthProvider>
      <Router>
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route
            path='/'
            element={
            <PrivateRoute>
              <HomePage />
            </PrivateRoute>
            }
          />
        </Routes>
      </Router>
    </AuthProvider>
  );
}

export default App;
