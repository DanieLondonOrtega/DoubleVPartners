import React, { useContext, useState, createContext, ReactNode } from "react";


interface User {
  email: string;
  role: string;
}

interface AuthContextProps {
  user: User | null;
  login: (email: string, password: string, token: string, role: string) => boolean;
  logout: () => void;
}

const AuthContext = createContext<AuthContextProps | undefined>(undefined);

export const useAuth = (): AuthContextProps => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};

interface AuthProviderProps {
  children: ReactNode;
}

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [user, setUser] = useState<User | null>(null);

  const login = (email: string, password: string, token:string, role : string ): boolean => {
    // Mock authentication based on email and password
    if(token == null){
      return false;
    }

    localStorage.setItem("auth",token);
    localStorage.setItem("email",email);
    setUser({ email, role });
    return true;
  };

  const logout = () => setUser(null);

  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
