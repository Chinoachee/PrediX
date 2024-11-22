import React from "react";
import { Button } from "antd";
import BaseHeader from "./BaseHeader"; // Базовая шапка
import { useNavigate } from "react-router-dom";

function LoginHeader() {
  const navigate = useNavigate();

  return (
    <BaseHeader>
      <span style={{ marginRight: "10px", color: "white" }}>Нет аккаунта?</span>
      <Button
        type="default"
        style={{
            width: "150px",
          backgroundColor: "#ffffff",
          color: "#000",
          borderColor: "#d9d9d9",
          boxShadow: "none",
        }}
        onClick={() => navigate("/register")}>
        Зарегистрироваться
      </Button>
    </BaseHeader>
  );
}

export default LoginHeader;
