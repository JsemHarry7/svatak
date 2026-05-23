"use client";

import React from "react";
import { useState, useEffect } from "react";

export default function Clock() {
  const [time, setTime] = useState("");
  useEffect(() => {
    const id = setInterval(() => {
      setTime(`${new Date().toLocaleTimeString()}`);
    }, 1000);
    return() => {
        clearInterval(id);
    } 
  }, []);
  return <h1>{time}</h1>;
}
