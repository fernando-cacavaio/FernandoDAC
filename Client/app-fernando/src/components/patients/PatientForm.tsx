import { NavLink, useNavigate, useParams } from "react-router-dom";
import { ChangeEvent, useEffect, useState } from "react";
import apiConnector from "../../api/apiConnector.ts";
import { Button, Form, Segment } from "semantic-ui-react";
import { PatientDTO } from "../../models/patientDTO.ts";

export default function PatientForm() {
  const { id } = useParams();
  const navigate = useNavigate();
  const jwttoken = sessionStorage.getItem("jwttoken");

  const [patient, setPatient] = useState<PatientDTO>({
    id: undefined,
    name: "",
    address: "",
    phone: "",
    sin: "",
  });

  useEffect(() => {
    if (!jwttoken) {
      navigate("/login");
    }
    if (id) {
      apiConnector
        .getPatientById(Number(id), jwttoken as string)
        .then((patient) => setPatient(patient!));
    }
  }, [id]);

  function handleSubmitPatient() {
    if (!patient.id) {
      apiConnector
        .createPatient(patient, jwttoken as string)
        .then(() => navigate("/"));
    } else {
      apiConnector
        .editPatient(patient, jwttoken as string)
        .then(() => navigate("/"));
    }
  }

  function handleInputChange(
    event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) {
    const { name, value } = event.target;
    setPatient({ ...patient, [name]: value });
  }

  return (
    <Segment clearing>
      <Form
        onSubmit={handleSubmitPatient}
        autoComplete="off"
        className="ui invereted form"
      >
        <Form.Input
          label="Name"
          placeholder="Name"
          name="name"
          value={patient.name}
          onChange={handleInputChange}
          maxLength="255"
          required={true}
        />
        <Form.Input
          label="SIN"
          placeholder="SIN"
          name="sin"
          value={patient.sin}
          onChange={handleInputChange}
          onKeyPress={(event: any) => {
            if (!/[0-9]/.test(event.key)) {
              event.preventDefault();
            }
          }}
          maxLength="9"
          minLength="9"
          required={true}
        />
        <Form.Input
          label="Address"
          placeholder="Address"
          name="address"
          value={patient.address}
          onChange={handleInputChange}
          maxLength="255"
        />
        <Form.Input
          label="Phone"
          placeholder="Phone"
          name="phone"
          value={patient.phone}
          onChange={handleInputChange}
          onKeyPress={(event: any) => {
            if (!/[0-9]/.test(event.key)) {
              event.preventDefault();
            }
          }}
          maxLength="8"
          minLength="8"
        />
        <Button positive type="submit" content="Submit" />
        <Button as={NavLink} to="/" type="button" content="Cancel" />
      </Form>
    </Segment>
  );
}
