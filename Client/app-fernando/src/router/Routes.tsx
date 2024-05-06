import { RouteObject, createBrowserRouter } from "react-router-dom";
import App from "../App";
import PatientForm from "../components/patients/PatientForm";
import Login from "../pages/Login";
import Register from "../pages/Register";

export const routes: RouteObject[] = [
  {
    path: "/",
    element: <App />,
    children: [
      { path: "createPatient", element: <PatientForm key="create" /> },
      { path: "editPatient/:id", element: <PatientForm key="edit" /> },
      { path: "login", element: <Login /> },
      { path: "register", element: <Register /> },
    ],
  },
];

export const router = createBrowserRouter(routes);
