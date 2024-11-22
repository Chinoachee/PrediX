import React from "react";
import { Layout, Form, Input, Button, Typography } from "antd";
import { useNavigate } from "react-router-dom";
import LoginHeader from "../components/LoginHeader";

const { Header, Content } = Layout;
const { Title } = Typography;

function Login() {
  const navigate = useNavigate();

  const onFinish = (values) => {
    console.log("Успешный вход:", values);
  };

  const onFinishFailed = (errorInfo) => {
    console.log("Ошибка при вводе данных:", errorInfo);
  };

  return (
    <Layout style={{ minHeight: "100vh", backgroundColor: "#2a2a2a" }}>
      {/* Шапка */}
      <LoginHeader/>

      {/* Основной контент */}
      <Content
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          padding: "20px",
        }}
      >
        <div
          style={{
            maxWidth: "400px",
            width: "100%",
            padding: "20px",
            backgroundColor: "#3a3a3a",
            borderRadius: "8px",
            boxShadow: "0px 4px 10px rgba(0, 0, 0, 0.3)",
          }}
        >
          <Title level={2} style={{ color: "white", textAlign: "center" }}>
            Войти
          </Title>
          <Form
            name="login"
            layout="vertical"
            onFinish={onFinish}
            onFinishFailed={onFinishFailed}
            requiredMark={false}
          >
            {/* Поле Login */}
            <Form.Item
              label={
                <span
                  style={{
                    position: "absolute",
                    top: "10px",
                    left: "-2px",
                    backgroundColor: "#3a3a3a",
                    padding: "0 5px",
                    color: "white",
                    fontSize: "13px",
                  }}
                >
                  Логин
                </span>
              }
              name="login"
              rules={[{ required: true, message: "Пожалуйста, введите логин!" }]}
              style={{ marginBottom: "12px", position: "relative" }}
            >
              <Input placeholder="Введите логин" />
            </Form.Item>

            {/* Поле Пароль */}
            <Form.Item
              label={
                <span
                  style={{
                    position: "absolute",
                    top: "10px",
                    left: "-2px",
                    backgroundColor: "#3a3a3a",
                    padding: "0 5px",
                    color: "white",
                    fontSize: "13px",
                  }}
                >
                  Пароль
                </span>
              }
              name="password"
              rules={[{ required: true, message: "Пожалуйста, введите пароль!" }]}
              style={{ marginBottom: "12px", position: "relative" }}
            >
              <Input.Password placeholder="Введите пароль" />
            </Form.Item>

            {/* Кнопка входа */}
            <Form.Item style={{ paddingTop: "15px" }}>
              <Button
                type="primary"
                htmlType="submit"
                style={{
                  width: "100%",
                  backgroundColor: "#ffffff",
                  color: "#000",
                  border: "none",
                }}
              >
                Войти
              </Button>
            </Form.Item>
          </Form>
        </div>
      </Content>
    </Layout>
  );
}

export default Login;
