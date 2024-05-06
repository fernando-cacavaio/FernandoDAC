import { Button, Form, Message, Segment } from "semantic-ui-react";
import { LoginUserDTO } from "../models/loginUserDTO";
import { ChangeEvent, useEffect, useState } from "react";
import apiConnector from "../api/apiConnector";
import { Link, useNavigate } from "react-router-dom";

const Login = () => {
  const navigate = useNavigate();
  const [error, setError] = useState<string>("");

  useEffect(() => {
    sessionStorage.clear();
  }, []);

  const [user, setUser] = useState<LoginUserDTO>({
    password: "",
    userName: "",
  });

  function handleInputChange(
    event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) {
    const { name, value } = event.target;
    setUser({ ...user, [name]: value });
  }

  const handleSubmitUser = async () => {
    try {
      await apiConnector.loginUser(user);
      navigate("/");
    } catch (error: any) {
      setError(error.response.data);
    }
  };

  return (
    <div style={{ display: "flex", justifyContent: "center" }}>
      <Segment clearing style={{ width: "50%" }}>
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
          <Button positive type="submit" content="Login" />
          <Link className="btn btn-success" to={"/register"}>
            New User
          </Link>
          <p></p>
          {error && (
            <Message negative>
              <p>{error}</p>
            </Message>
          )}{" "}
          {/* Display error message if exists */}
        </Form>
      </Segment>
    </div>
  );
};

export default Login;
