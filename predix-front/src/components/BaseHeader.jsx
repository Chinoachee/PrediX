import React from "react";
import { useNavigate } from "react-router-dom";

function BaseHeader({ children }) {
  const navigate = useNavigate();

  return (
    <header
      style={{
        display: "flex",
        justifyContent: "space-between",
        alignItems: "center",
        padding: "0 20px",
        height: "56px",
        backgroundColor: "#1a1a1a",
      }}
    >
      {/* Логотип */}
      <div
        onClick={() => navigate("/")}
        style={{
          color: "white",
          fontSize: "20px",
          fontWeight: "bold",
          cursor: "pointer",
        }}
      >
        PrediX
      </div>

      {/* Контент справа */}
      <div style={{ display: "flex", alignItems: "center" }}>{children}</div>
    </header>
  );
}

export default BaseHeader;
