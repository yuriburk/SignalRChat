import React, { useEffect } from "react";
import Chat from "../../components/Chat";

function Main() {
  useEffect(() => notifyMe());

  function notifyMe() {
    if (!("Notification" in window)) {
      alert("This browser does not support desktop notification");
    }

    if (Notification.permission !== "granted") {
      Notification.requestPermission();
    }
  }

  return (
    <div className="App">
      <Chat />
    </div>
  );
}

export default Main;
