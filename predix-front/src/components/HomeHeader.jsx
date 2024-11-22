import React from "react";
import { Button } from "antd";
import BaseHeader from "./BaseHeader"; // Базовая шапка
import { useNavigate } from "react-router-dom";

function HomeHeader() {
  const navigate = useNavigate();

  return (
    <BaseHeader>
      <Button
        type="default"
        style={{
          marginRight: "10px",
          backgroundColor: "#ffffff",
          color: "#000",
          borderColor: "#d9d9d9",
          boxShadow: "none",
        }}
        onClick={() => navigate("/register")}
      >
        Регистрация
      </Button>
      <Button
        style={{
          backgroundColor: "#2a2a2a",
          color: "white",
          borderColor: "#2a2a2a",
          boxShadow: "none",
        }}
        onClick={() => navigate("/login")}
      >
        Авторизация
      </Button>
    </BaseHeader>
  );
}

export default HomeHeader;
