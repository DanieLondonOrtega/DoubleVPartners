import axios from "axios";
import React, { useEffect, useState } from "react";
import { Button, Form, Modal, Table } from "react-bootstrap";
interface Task {
  idTask: number;
  user: number; 
  nameTask: string;
  description: string;
  statusTask: string;
  createDate: string;
}

const AgentDashboard: React.FC = () => {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [show, setShow] = useState(false);
  const [taskId, setTaskId] = useState<number | null>(null);
  const [statusTask, setStatus] = useState("");
  const [taskIdUser, setUserTaskId] = useState<number | null>(null);

  useEffect(() => {
    fetchTasks();
  }, []);

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
  const handleEditTask = (task: Task) => {
    setTaskId(task.idTask!);
    setUserTaskId(task.user!);
    setStatus(task.statusTask);
    setShow(true);
  };

  const handleUpdateTask = async () => {
    if (taskId !== null) {
      const updatedTask = {
        idTask: taskId,
        taskIdUser,
        statusTask,        
      };

      try {
        const response = await axios.put(
          "https://localhost:7198/api/Task/changesAssign",
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

  return (
    <div className="dashboard-container">
      <div>
        <h2 className="text-center mb-4">Supervidor Task Management</h2>
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
                    Assign
                  </Button>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
        <Modal show={show} onHide={() => setShow(false)}>
          <Modal.Header closeButton>
            <Modal.Title>
               Assign Task
            </Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form>
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
            </Form>
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={() => setShow(false)}>
              Cancel
            </Button>
            <Button
              variant="primary"
              onClick={handleUpdateTask}
            >
             {"Update Task"}
            </Button>
          </Modal.Footer>
        </Modal>
      </div>
    </div>
  );
};

export default AgentDashboard;
