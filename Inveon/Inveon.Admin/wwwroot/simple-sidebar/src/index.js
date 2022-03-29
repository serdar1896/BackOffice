import React from "react";

const DEFAULT_STYLES = {
  sidebar: {
    position: "relative",
  },
  content: {
    zIndex: "1",
    transition: "transform 0.2s ease-in-out",
    transform: "translateX(-100%)",
    position: "absolute",
    background: "#ffffff",
    width: "250px",
    height: "100%",
  },
  overlay: {
    pointerEvents: "none",
    transition: "opacity 0.3s ease-in-out",
    position: "absolute",
    background: "rgba(255, 255, 255, 0.8)",
    width: "100%",
    height: "100%",
    opacity: "0",
  },
};

const OPEN_STYLES = {
  content: {
    transform: "translateX(0)",
  },
  overlay: {
    pointerEvents: "auto",
    opacity: "1",
  },
};

const Sidebar = ({ isOpen, onClose, content, children }) => {
  return (
    <div
      style={{
        ...DEFAULT_STYLES.sidebar,
        ...(isOpen ? OPEN_STYLES.sidebar : {}),
      }}
    >
      <div
        style={{
          ...DEFAULT_STYLES.content,
          ...(isOpen ? OPEN_STYLES.content : {}),
        }}
      >
        {content}
      </div>
      <div
        onClick={onClose}
        style={{
          ...DEFAULT_STYLES.overlay,
          ...(isOpen ? OPEN_STYLES.overlay : {}),
        }}
      />
      {children}
    </div>
  );
};

export default Sidebar;
