import axios from "axios";
import React, { ChangeEvent, useEffect, useState } from "react";
import { Button, Form, Modal, Table } from "react-bootstrap";

interface Task {
  idTask: number;
  user: string;
  nameTask: string;
  description: string;
  statusTask: string;
  createDate: string;
}

interface User {
  idUser: number;
  idRole: number;
  name: string;
  email: string;
  phoneNumber: string;
  password: string;
  isActive: boolean;
}

const AdminDashboard: React.FC = () => {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [users, setUsers] = useState<User[]>([]);
  const [show, setShow] = useState(false);
  const [isEdit, setIsEdit] = useState(false);
  const [showUser, setShowUser] = useState(false);
  const [isEditUser, setIsEditUser] = useState(false);


  const [nameTask, setName] = useState("");
  const [description, setDescription] = useState("");
  const [statusTask, setStatus] = useState("");
  const [createDate, setCreateDate] = useState("");
  const [taskId, setTaskId] = useState<number | null>(null);

  const [name, setNameUser] = useState("");
  const [email, setEmail] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");
  const [password, setPassword] = useState("");
  const [isActive, setIsActive] = useState(false);
  const [userId, setUserId] = useState<number | null>(null);
  const [idRole, setRoleId] = useState<number | null>(null);

  const fetchTasks = async () => {
    try {
      const response = await fetch("https://localhost:7198/api/Task", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("auth")}`,
        },
      });
      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }

      const data = await response.json();
      setTasks(data);
    } catch (error) {
      console.error("Error fetching tasks:", error);
    }
  };

  const fetchUsers = async () => {
    try {
      const response = await fetch("https://localhost:7198/api/User", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("auth")}`,
        },
      });
      if (!response.ok) {
        throw new Error(`Error: ${response.statusText}`);
      }

      const data = await response.json();
      setUsers(data);
    } catch (error) {
      console.error("Error fetching user:", error);
    }
  };

  useEffect(() => {
    fetchTasks();
    fetchUsers();
  }, []);

  const handleDeleteTask = async (taskId: number) => {
    try {
      const response = await axios.delete(
        `https://localhost:7198/api/Task/${taskId}`,
        {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("auth")}`,
            "Content-Type": "application/json",
          },
        }
      );
      setTasks(tasks.filter((task) => task.idTask !== taskId));
    } catch (error) {
      console.error("Error deleting task:", error);
    }
  };

  const handleDeleteUser = async (userId: number) => {
    try {
      const response = await axios.delete(
        `https://localhost:7198/api/User/${taskId}`,
        {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("auth")}`,
            "Content-Type": "application/json",
          },
        }
      );
      setUsers(users.filter((user) => user.idUser !== userId));
    } catch (error) {
      console.error("Error deleting user:", error);
    }
  };

  const handleEditTask = (task: Task) => {
    setTaskId(task.idTask!);
    setName(task.nameTask);
    setDescription(task.description);
    setStatus(task.statusTask);
    setCreateDate(task.createDate);
    setIsEdit(true);
    setShow(true);
  };

    const handleEditUser = (user: User) => {
      setUserId(user.idUser!);
      setRoleId(user.idRole);
      setNameUser(user.name);
      setEmail(user.email);
      setPhoneNumber(user.phoneNumber);
      setPassword(user.password);
      setIsActive(user.isActive);
      setIsEditUser(true);
      setShowUser(true);
    };


  const handleCreateTask = () => {
    setTaskId(null);
    setName("");
    setDescription("");
    setStatus("");
    setCreateDate("");
    setIsEdit(false);
    setShow(true);
  };

  const handleCreateUser = () => {
    setUserId(null);
    setRoleId(null);
    setNameUser("");
    setEmail("");
    setPhoneNumber("");
    setPassword("");
    setIsActive(false);
    setIsEditUser(false);
    setShowUser(true);
  };

  const handleUpdateTask = async () => {
    if (taskId !== null) {
      const updatedTask = {
        idTask: taskId,
        nameTask,
        description,
        statusTask,
        createDate,
      };

      try {
        const response = await axios.put(
          "https://localhost:7198/api/Task",
          updatedTask,
          {
            headers: {
              Authorization: `Bearer ${localStorage.getItem("auth")}`,
              "Content-Type": "application/json",
            },
          }
        );

        fetchTasks();
      } catch (error) {
        console.error("Error updating task:", error);
      }
    }
    setShow(false);
  };

  const handleUpdateUser = async () => {
    if (userId !== null) {
      const updatedUser = {
        idUser: userId,
        idRole,
        name,
        email,
        phoneNumber,
        password,
        isActive,
      };

      try {
        const response = await axios.put(
          "https://localhost:7198/api/User",
          updatedUser,
          {
            headers: {
              Authorization: `Bearer ${localStorage.getItem("auth")}`,
              "Content-Type": "application/json",
            },
          }
        );

        fetchUsers();
      } catch (error) {
        console.error("Error updating user:", error);
      }
    }
    setShow(false);
  };


  const handleCreateNewTask = async () => {
    const newTask = {
      nameTask,
      description,
      statusTask,
      createDate,
    };

    try {
      const response = await axios.post(
        "https://localhost:7198/api/Task",
        newTask,
        {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("auth")}`,
            "Content-Type": "application/json",
          },
        }
      );
      fetchTasks();
    } catch (error) {
      console.error("Error creating task:", error);
    }
    setShow(false);
  };

  const handleCreateNewUser = async () => {
    const newUser = {
      idRole,
      name,
      email,
      phoneNumber,
      password,
      isActive,
    };

    try {
      const response = await axios.post(
        "https://localhost:7198/api/User",
        newUser,
        {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("auth")}`,
            "Content-Type": "application/json",
          },
        }
      );
      fetchUsers();
    } catch (error) {
      console.error("Error creating users:", error);
    }
    setShow(false);
  };
  return (
    <div className="dashboard-container">
      <div>
        <h2 className="text-center mb-4">Admin Task Management</h2>
        <Button className="mb-4" variant="primary" onClick={handleCreateTask}>
          Create New Task
        </Button>
        <Table striped bordered hover>
          <thead>
            <tr>
              <th>Task</th>
              <th>Name</th>
              <th>Description</th>
              <th>Status Task</th>
              <th>Create Date</th>
            </tr>
          </thead>
          <tbody>
            {tasks.map((task) => (
              <tr key={task.idTask}>
                <td>{task.idTask}</td>
                <td>{task.nameTask}</td>
                <td>{task.description}</td>
                <td>{task.statusTask}</td>
                <td>{new Date(task.createDate).toLocaleDateString()}</td>
                <td>
                  <Button
                    variant="warning"
                    className="mr-2"
                    onClick={() => handleEditTask(task)}
                  >
                    Edit
                  </Button>
                  <Button
                    variant="danger"
                    onClick={() => handleDeleteTask(task.idTask)}
                  >
                    Delete
                  </Button>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
        <Modal show={show} onHide={() => setShow(false)}>
          <Modal.Header closeButton>
            <Modal.Title>
              {isEdit ? "Edit Task" : "Create New Task"}
            </Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form>
              <Form.Group controlId="formTaskName">
                <Form.Label>Task Name</Form.Label>
                <Form.Control
                  type="text"
                  value={nameTask}
                  onChange={(e) => setName(e.target.value)}
                  placeholder="Enter task name"
                />
              </Form.Group>
              <Form.Group controlId="formTaskDescription">
                <Form.Label>Description</Form.Label>
                <Form.Control
                  as="textarea"
                  value={description}
                  onChange={(e) => setDescription(e.target.value)}
                  placeholder="Enter task description"
                />
              </Form.Group>
              <Form.Group controlId="formTaskStatus">
                <Form.Label>Status</Form.Label>
                <Form.Select
                  value={statusTask}
                  onChange={(e) => setStatus(e.target.value)}
                >
                  <option>Select</option>
                  <option value="Pendiente">Pendiente</option>
                  <option value="EnProceso">En Proceso</option>
                  <option value="Completada">Completada</option>
                </Form.Select>
              </Form.Group>
              <Form.Group controlId="formTaskCreateDate">
                <Form.Label>Create Date</Form.Label>
                <Form.Control
                  type="date"
                  value={createDate}
                  onChange={(e) => setCreateDate(e.target.value)}
                />
              </Form.Group>
            </Form>
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={() => setShow(false)}>
              Cancel
            </Button>
            <Button
              variant="primary"
              onClick={isEdit ? handleUpdateTask : handleCreateNewTask}
            >
              {isEdit ? "Update Task" : "Create Task"}
            </Button>
          </Modal.Footer>
        </Modal>
      </div>
      <div>
        <br></br>
        <h2 className="text-center mb-4">Admin User Management</h2>
        <Button className="mb-4" variant="primary" onClick={handleCreateUser}>
          Create New User
        </Button>
        <Table striped bordered hover>
          <thead>
            <tr>
              <th>User</th>
              <th>Role</th>
              <th>Name</th>
              <th>Email</th>
              <th>Phone Number</th>
              <th>Activo</th>
            </tr>
          </thead>
          <tbody>
            {users.map((user) => (
              <tr key={user.idUser}>
                <td>{user.idUser}</td>
                <td>{user.idRole === 1 ? 'Administrador' : user.idRole === 2 ? 'Supervisor' : 'Empleado'}</td>
                <td>{user.name}</td>
                <td>{user.email}</td>
                <td>{user.phoneNumber}</td>
                <td>{user.isActive}</td>
                <td>
                  <Button
                    variant="warning"
                    className="mr-2"
                    onClick={() => handleEditUser(user)}
                  >
                    Edit
                  </Button>
                  <Button
                    variant="danger"
                    onClick={() => handleDeleteUser(user.idUser)}
                  >
                    Delete
                  </Button>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
        <Modal show={showUser} onHide={() => setShowUser(false)}>
          <Modal.Header closeButton>
            <Modal.Title>
              {isEditUser ? "Edit User" : "Create New User"}
            </Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form>
              <Form.Group controlId="formUserRole">
                <Form.Label>Role</Form.Label>
                <Form.Select
                  value={idRole!}
                  onChange={(e) => setRoleId(parseInt(e.target.value))}
                >
                  <option>Select</option>
                  <option value="1">Administrador</option>
                  <option value="2">Supervisor</option>
                  <option value="3">Empleado</option>
                </Form.Select>
              </Form.Group>
              <Form.Group controlId="formUserName">
                <Form.Label>User Name</Form.Label>
                <Form.Control
                  type="text"
                  value={name}
                  onChange={(e) => setNameUser(e.target.value)}
                  placeholder="Enter user name"
                />
              </Form.Group>
              <Form.Group controlId="formUserEmail">
                <Form.Label>Email</Form.Label>
                <Form.Control
                  type="text"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  placeholder="Enter user email"
                />
              </Form.Group>
              <Form.Group controlId="formUserPhoneNumber">
                <Form.Label>Phone Number</Form.Label>
                <Form.Control
                  type="text"
                  value={phoneNumber}
                  onChange={(e) => setPhoneNumber(e.target.value)}
                  placeholder="Enter user phone number"
                />
              </Form.Group>
              <Form.Group controlId="formUserPassword">
                <Form.Label>Password</Form.Label>
                <Form.Control
                  type="text"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  placeholder="Enter user password"
                />
              </Form.Group>
            </Form>
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={() => setShowUser(false)}>
              Cancel
            </Button>
            <Button
              variant="primary"
              onClick={isEditUser ? handleUpdateUser : handleCreateNewUser}
            >
              {isEditUser ? "Update User" : "Create User"}
            </Button>
          </Modal.Footer>
        </Modal>
      </div>
    </div>
  );
};

export default AdminDashboard;
