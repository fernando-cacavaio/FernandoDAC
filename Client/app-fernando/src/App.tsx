import { Outlet, useLocation } from "react-router-dom";
import "./App.css";
import { Container } from "semantic-ui-react";
import PatientTable from "./components/patients/PatientTable";

function App() {
  const location = useLocation();

  return (
    <>
      {location.pathname === "/" ? (
        <PatientTable />
      ) : (
        <Container className="container-style">
          <Outlet />
        </Container>
      )}
    </>
  );
}

export default App;
