import { Button } from "semantic-ui-react";
import { PatientDTO } from "../../models/patientDTO";
import apiConnector from "../../api/apiConnector";
import { NavLink } from "react-router-dom";

interface Props {
  patient: PatientDTO;
}

export default function PatientTableItem({ patient }: Props) {
  const jwttoken = sessionStorage.getItem("jwttoken");
  function addDash(text: string) {
    let finalVal = text == null ? "" : text.replace(/(\d{4})/, "$1-");
    return finalVal;
  }

  function numberWithSpaces(text: string) {
    return text.replace(/\B(?=(\d{3})+(?!\d))/g, " ");
  }

  return (
    <>
      <tr className="center aligned">
        <td data-label="Name">{patient.name}</td>
        <td data-label="SIN">{numberWithSpaces(patient.sin)}</td>
        <td data-label="Phone">{patient.address}</td>
        <td data-label="Address">{addDash(patient.phone)}</td>
        <td data-label="Action">
          <Button
            as={NavLink}
            to={`editPatient/${patient.id}`}
            color="yellow"
            type="submit"
          >
            Edit
          </Button>
          <Button
            type="button"
            negative
            onClick={async () => {
              await apiConnector.deletePatient(patient.id!, jwttoken as string);
              window.location.reload();
            }}
          >
            Delete
          </Button>
        </td>
      </tr>
    </>
  );
}
