import React from "react";
import { Layout, Form, Input, Button, Typography } from "antd";
import { useNavigate } from "react-router-dom"; // Импорт useNavigate
import RegisterHeader from "../components/RegisterHeader";

const { Header, Content } = Layout;
const { Title } = Typography;

function Register() {
  const navigate = useNavigate(); // Навигация

  const onFinish = (values) => {
    console.log("Успешная регистрация:", values);
  };

  const onFinishFailed = (errorInfo) => {
    console.log("Ошибка при вводе данных:", errorInfo);
  };

  return (
    <Layout style={{ minHeight: "100vh", backgroundColor: "#2a2a2a" }}>
      {/* Шапка */}
      <RegisterHeader/>
      
      {/* Текст и ссылка */}
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
            backgroundColor: "#3a3a3a", // Чуть светлее основного фона
            borderRadius: "8px",
            boxShadow: "0px 4px 10px rgba(0, 0, 0, 0.3)",
          }}
        >
          <Title level={2} style={{ color: "white", textAlign: "center" }}>
            Регистрация
          </Title>
<Form
  name="register"
  layout="vertical"
  onFinish={onFinish}
  onFinishFailed={onFinishFailed}
  requiredMark={false} // Отключаем красные точки
>

{/* Ввод логина */}
<Form.Item
  label={
    <span style={{ 
      position: "absolute", 
      top: "10px", 
      left: "-2px", 
      backgroundColor: "#3a3a3a", // Цвет фона, совпадающий с цветом формы
      padding: "0 5px", 
      color: "white", 
      fontSize: "13px"
    }}>
      Логин
    </span>
  }
  name="username"
  rules={[{ required: true, message: "Пожалуйста, введите логин!" }]}
  style={{ marginBottom: "12px", position: "relative" }}
>
  <Input placeholder="Введите логин" />
</Form.Item>
{/* Ввод почты */}
<Form.Item
  label={
    <span style={{ 
      position: "absolute", 
      top: "10px", 
      backgroundColor: "#3a3a3a", 
      padding: "0 5px", 
      color: "white", 
      left: "-2px", 
      fontSize: "13px"
    }}>
      Email
    </span>
  }
  name="email"
  rules={[
    { required: true, message: "Пожалуйста, введите email!" },
    { type: "email", message: "Введите корректный email!" },
  ]}
  style={{ marginBottom: "12px", position: "relative" }}
>
  <Input placeholder="Введите email" />
</Form.Item>
{/* Ввод пароля */}
<Form.Item
  label={
    <span style={{ 
      position: "absolute", 
      top: "10px", 
      left: "-2px", 
      backgroundColor: "#3a3a3a", 
      padding: "0 5px", 
      color: "white", 
      fontSize: "13px"
    }}>
      Пароль
    </span>
  }
  name="password"
  rules={[
    { required: true, message: "Пожалуйста, введите пароль!" },
    { min: 6, message: "Пароль должен быть не менее 6 символов!" },
  ]}
  style={{ marginBottom: "12px", position: "relative" }}
>
  <Input.Password placeholder="Введите пароль" />
</Form.Item>

{/* Кнопка отправки */}
<Form.Item style={{ paddingTop: "15px" }}>
     {/* Отступ внутри элемента */}
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
    Зарегистрироваться
  </Button>
</Form.Item>
</Form>
        </div>
      </Content>
    </Layout>
  );
}

export default Register;
