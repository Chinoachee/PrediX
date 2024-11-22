import React from "react";
import { Layout } from "antd";
import HomeHeader from "../components/HomeHeader"; // Импортируем созданный компонент шапки

const { Content } = Layout;

function Home() {
  return (
    <Layout style={{ minHeight: "100vh", width: "100vw" }}>
      {/* Шапка */}
      <HomeHeader />

      {/* Основной контент */}
      <Content
        style={{
          padding: "20px",
          backgroundColor: "#2a2a2a", // Темнее, чем body, но светлее, чем header
          color: "white", // Белый текст для контраста
        }}
      >
        <h2>Добро пожаловать на PrediX!</h2>
        <p>Пример простого списка:</p>
        <ul>
          <li>Элемент списка 1</li>
          <li>Элемент списка 2</li>
          <li>Элемент списка 3</li>
        </ul>
      </Content>
    </Layout>
  );
}

export default Home;
