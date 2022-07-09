import React, { useEffect, useState } from "react";
import { createAccountStyle } from "./style";
import {
  Breadcrumbs,
  Link,
  Typography,
  Button,
  TextField,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
} from "@material-ui/core";
import * as Yup from "yup";
import { Formik } from "formik";
import ValidationErrorMessage from "../../components/ValidationErrorMessage/index";
import authService from "../../service/auth.service";
import { useHistory } from "react-router-dom";
import { toast } from "react-toastify";
import { CreateUserModel } from "../../models/AuthModel";
import { Role, RoutePaths } from "../../utils/enum";
import RoleModel from "../../models/RoleModel";
import { materialCommonStyles } from "../../utils/materialCommonStyles";
import BaseList from "../../models/BaseList";
import userService from "../../service/user.service";
import { AuthContextModel, useAuthContext } from "../../context/auth";
import UserModel from "../../models/UserModel";


const Register: React.FC = () => {
  const classes = createAccountStyle();
  const materialClasses = materialCommonStyles();
  const history = useHistory();
  const initialValues: UserModel = new UserModel();
  const [roleList, setRoleList] = useState<RoleModel[]>([]);
  const authContext: AuthContextModel = useAuthContext();

  useEffect(() => {
    getRoles();
  }, []);

  const getRoles = (): void => {
    userService.getAllRoles().then((res: BaseList<RoleModel[]>) => {
      if (res.results.length) {
        setRoleList(
          res.results.filter((role: RoleModel) => role.id !== Role.Admin)
        );
        
      }
    });
  };
  

  const validationSchema = Yup.object().shape({
    email: Yup.string()
      .email("Invalid email address format")
      .required("Email is required"),
    password: Yup.string()
      .min(5, "Password must be 5 characters at minimum")
      .required("Password is required"),
    confirmPassword: Yup.string()
      .oneOf(
        [Yup.ref("password"), null],
        "Password and Confirm Password must be match."
      )
      .required("Confirm Password is required."),
    firstName: Yup.string().required("First name is required"),
    lastName: Yup.string().required("Last name is required"),
    roleId: Yup.number().required("Role is required"),
  });

  const onSubmit = (data: UserModel): void => {
    data.id=authContext.user.id;
    data.roleId=authContext.user.roleId;
    console.log(data);
    userService.updateProfile(data).then((res) => {
      history.push(RoutePaths.BookListing);
      toast.success("Successfully registered");
    });
  };
  return (
    <div className={classes.createAccountWrapper}>
      <div className="create-account-page-wrapper">
        <div className="container">
          <Typography variant="h1">Update your profile</Typography>
          <div className="create-account-row">
            <Formik
              initialValues={initialValues}
              validationSchema={validationSchema}
              onSubmit={onSubmit}
            >
              {({
                values,
                errors,
                touched,
                handleBlur,
                handleChange,
                handleSubmit,
              }) => (
                <form onSubmit={handleSubmit}>
                  <div className="form-block">
                    <div className="personal-information">
                      <div className="form-row-wrapper">
                        <div className="form-col">
                          <TextField
                            id="first-name"
                            name="firstName"
                            label="First Name *"
                            variant="outlined"
                            inputProps={{ className: "small" }}
                            onBlur={handleBlur}
                            onChange={handleChange}
                            value={authContext.user.firstName}
                          />
                          <ValidationErrorMessage
                            message={errors.firstName}
                            touched={touched.firstName}
                          />
                        </div>
                        <div className="form-col">
                          <TextField
                            onBlur={handleBlur}
                            onChange={handleChange}
                            id="last-name"
                            name="lastName"
                            label="Last Name *"
                            variant="outlined"
                            inputProps={{ className: "small" }}
                          />
                          <ValidationErrorMessage
                            message={errors.lastName}
                            touched={touched.lastName}
                          />
                        </div>
                        <div className="form-col">
                          <TextField
                            onBlur={handleBlur}
                            onChange={handleChange}
                            id="email"
                            name="email"
                            label="Email Address *"
                            variant="outlined"
                            inputProps={{ className: "small" }}
                          />
                          <ValidationErrorMessage
                            message={errors.email}
                            touched={touched.email}
                          />
                        </div>
                        
                      </div>
                    </div>
                    <div className="login-information">
                      <div className="form-row-wrapper">
                        <div className="form-col">
                          <TextField
                            onBlur={handleBlur}
                            onChange={handleChange}
                            id="password"
                            type="password"
                            name="password"
                            label="Password *"
                            variant="outlined"
                            inputProps={{ className: "small" }}
                          />
                          <ValidationErrorMessage
                            message={errors.password}
                            touched={touched.password}
                          />
                        </div>
                        <div className="form-col">
                          <TextField
                            type="password"
                            onBlur={handleBlur}
                            onChange={handleChange}
                            id="confirm-password"
                            name="confirmPassword"
                            label="Confirm Password *"
                            variant="outlined"
                            inputProps={{ className: "small" }}
                          />
                          <ValidationErrorMessage
                            message={errors.confirmPassword}
                            touched={touched.confirmPassword}
                          />
                        </div>
                      </div>
                      <div className="btn-wrapper">
                        <Button
                          className="pink-btn btn"
                          variant="contained"
                          type="submit"
                          color="primary"
                          disableElevation
                        >
                          Save
                        </Button>
                        <Button
                          className="pink-btn btn"
                          variant="contained"
                          color="primary"
                          disableElevation
                          onClick={()=>{history.push("/");}}
                          style={{margin: "0px 15px"}}
                        >
                          Cancel
                        </Button>
                      </div>
                    </div>
                  </div>
                </form>
              )}
            </Formik>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Register;
