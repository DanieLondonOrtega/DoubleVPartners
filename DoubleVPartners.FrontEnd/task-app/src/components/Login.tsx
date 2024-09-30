import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import { Form, Button, InputGroup, Alert } from "react-bootstrap";
import "../styles/index.css"; // Import custom styles
import axios from "axios";

const Login: React.FC = () => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [error, setError] = useState<string | null>(null);
  const { login } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    try {
      const response = await axios.post(
        "https://localhost:7198/api/Security",
        { email, password },
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
      if (response.data.data) {
        let typeRole: string;
        if (response.data.idRole === 1) {
          typeRole = "admin";
        } else if (response.data.idRole === 2) {
          typeRole = "agent";
        } else {
          typeRole = "employee";
        }

        login(email, password, response.data.data, typeRole);
        navigate(`/${typeRole}`);
      }
    } catch (error) {
      console.log(error);
      setError("Authentication failed. Please check your credentials.");
    }
  };

  return (
    <div className="form-container">
      <div className="form-box">
        <div className="form-header">
          <h2>Login</h2>
        </div>

        {error && <Alert variant="danger">{error}</Alert>}

        <Form onSubmit={handleSubmit}>
          {/* Email Input */}
          <Form.Group controlId="formEmail" className="form-group">
            <InputGroup>
              <InputGroup.Text>
                <i className="fas fa-envelope"></i> {/* Font Awesome icon */}
              </InputGroup.Text>
              <Form.Control
                type="email"
                placeholder="Enter email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
              />
            </InputGroup>
          </Form.Group>

          {/* Password Input */}
          <Form.Group controlId="formPassword" className="form-group">
            <InputGroup>
              <InputGroup.Text>
                <i className="fas fa-lock"></i> {/* Font Awesome icon */}
              </InputGroup.Text>
              <Form.Control
                type="password"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
              />
            </InputGroup>
          </Form.Group>

          {/* Submit Button */}
          <Button variant="primary" type="submit" className="submit-btn">
            Log In
          </Button>
        </Form>
      </div>
    </div>
  );
};

export default Login;
