import { useEffect, useState } from "react";
import { PatientDTO } from "../../models/patientDTO";
import apiConnector from "../../api/apiConnector";
import { Button, Container } from "semantic-ui-react";
import PatientTableItem from "./PatientTableItem";
import { NavLink, useNavigate } from "react-router-dom";

export default function PatientTable() {
  const navigate = useNavigate();
  const [patients, setPatients] = useState<PatientDTO[]>([]);
  const jwttoken = sessionStorage.getItem("jwttoken");

  useEffect(() => {
    const fetchData = async () => {
      const fetchedPatients = await apiConnector.getPatients(
        jwttoken as string
      );
      setPatients(fetchedPatients);
    };

    if (!jwttoken) {
      navigate("/login");
    }

    fetchData();
  }, []);

  const Logout = () => {
    sessionStorage.clear();
    navigate("/login");
  };
  return (
    <>
      <Container className="container-style">
        <div style={{ paddingBottom: "50px" }}>
          <Button onClick={Logout} floated="right" primary>
            Logout
          </Button>
        </div>
        <table className="ui table">
          <thead style={{ textAlign: "center" }}>
            <tr>
              <th>Name</th>
              <th>SIN</th>
              <th>Address</th>
              <th>Phone</th>
              <th>Health Status</th>
              <th>Action</th>
            </tr>
          </thead>
          <tbody>
            {patients.length != 0 &&
              patients.map((patient, index) => (
                <PatientTableItem
                  key={index}
                  patient={patient}
                ></PatientTableItem>
              ))}
          </tbody>
        </table>
        <Button
          as={NavLink}
          to="createPatient"
          floated="right"
          type="button"
          content="Create Patient"
          positive
        />
      </Container>
    </>
  );
}
