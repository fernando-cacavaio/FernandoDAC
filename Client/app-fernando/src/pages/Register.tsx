import {
  Button,
  Dropdown,
  DropdownProps,
  Form,
  Message,
  Segment,
  Select,
} from "semantic-ui-react";
import { LoginUserDTO } from "../models/loginUserDTO";
import { ChangeEvent, useEffect, useState } from "react";
import apiConnector from "../api/apiConnector";
import { Link, useNavigate } from "react-router-dom";
import { RegisterUserDTO } from "../models/registerUserDTO";

const Register = () => {
  const navigate = useNavigate();
  const [error, setError] = useState<string>("");

  const optionsRole = [{ key: "admin", text: "Admin", value: "admin" }];

  useEffect(() => {
    sessionStorage.clear();
  }, []);

  const [user, setUser] = useState<RegisterUserDTO>({
    password: "",
    userName: "",
    role: "",
  });

  function handleInputChange(
    event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) {
    const { name, value } = event.target;
    setUser({ ...user, [name]: value });
  }

  const handleSubmitUser = async () => {
    try {
      await apiConnector.createUser(user);
      navigate("/");
    } catch (error: any) {
      setError(error.response.data);
    }
  };

  const handleChange = (
    event: React.SyntheticEvent<HTMLElement, Event>,
    data: DropdownProps
  ) => {
    const { value } = data;
    setUser((prevUser) => ({ ...prevUser, role: value as string }));
  };

  return (
    <Segment clearing>
      <Form
        onSubmit={handleSubmitUser}
        autoComplete="off"
        className="ui invereted form"
      >
        <Form.Input
          label="Username"
          placeholder="username"
          name="userName"
          value={user.userName}
          onChange={handleInputChange}
          maxLength="100"
          required={true}
        />
        <Form.Input
          label="Password"
          placeholder="password"
          name="password"
          value={user.password}
          onChange={handleInputChange}
          maxLength="255"
          type="password"
          required={true}
        />
        <Form.Select
          fluid
          label="Role"
          placeholder="Role"
          options={optionsRole}
          required={true}
          value={user.role}
          onChange={handleChange}
        />
        <p></p>
        <Button positive type="submit" content="Create" />
        <p></p>
        {error && (
          <Message negative>
            <p>{error}</p>
          </Message>
        )}{" "}
        {/* Display error message if exists */}
      </Form>
    </Segment>
  );
};

export default Register;
