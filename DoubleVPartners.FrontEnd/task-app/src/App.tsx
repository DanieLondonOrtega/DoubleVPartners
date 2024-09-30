import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { AuthProvider } from "./context/AuthContext";
import PrivateRoute from "./components/PrivateRoute";
import Login from "./components/Login";
import AdminDashboard from "./components/AdminDashboard";
import AgentDashboard from "./components/AgentDashboard";
import EmployeeDashboard from "./components/EmployeeDashboard";

const App: React.FC = () => {
  return (
    <AuthProvider>
      <Router>
        <Routes>


          {/* Admin dashboard protected by role */}
          <Route
            path="/admin"
            element={
              <PrivateRoute roles={["admin"]} component={AdminDashboard} />
            }
          />

          {/* Agent dashboard protected by role */}
          <Route
            path="/agent"
            element={
              <PrivateRoute roles={["agent"]} component={AgentDashboard} />
            }
          />

          {/* Employee dashboard protected by role */}
          <Route
            path="/employee"
            element={
              <PrivateRoute
                roles={["employee"]}
                component={EmployeeDashboard}
              />
            }
          />

          {/* Default route */}
          <Route path="/" element={<Login />} />
        </Routes>
      </Router>
    </AuthProvider>
  );
};

export default App;
