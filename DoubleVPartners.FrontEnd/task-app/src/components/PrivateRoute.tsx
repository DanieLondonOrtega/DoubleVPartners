import React from "react";
import { Navigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

interface PrivateRouteProps {
  roles: string[];
  component: React.ComponentType<any>;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({
  component: Component,
  roles,
}) => {
  const { user } = useAuth();

  if (!user || !roles.includes(user.role as string)) {
    return <Navigate to="/" />;
  }

  return <Component />;
};

export default PrivateRoute;
