import React from "react";
import { Button } from "antd";
import BaseHeader from "./BaseHeader"; // Базовая шапка
import { useNavigate } from "react-router-dom";

function RegisterHeader() {
  const navigate = useNavigate();

  return (
    <BaseHeader>
      <span style={{ marginRight: "10px", color: "white" }}>Уже есть аккаунт?</span>
      <Button
        type="default"
        style={{
          width: "150px",
          backgroundColor: "#2a2a2a",
          color: "white",
          borderColor: "#2a2a2a",
          boxShadow: "none",
        }}
        onClick={() => navigate("/login")}
      >
        Войти
      </Button>
    </BaseHeader>
  );
}

export default RegisterHeader;
