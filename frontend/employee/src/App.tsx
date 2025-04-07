import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import Employee from './pages/EmployeeDetail';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/employee/:id" element={<Employee />} />
      </Routes>
    </Router>
  );
}

export default App;
