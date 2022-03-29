"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports["default"] = void 0;

var _react = _interopRequireDefault(require("react"));

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { "default": obj }; }

function ownKeys(object, enumerableOnly) { var keys = Object.keys(object); if (Object.getOwnPropertySymbols) { var symbols = Object.getOwnPropertySymbols(object); if (enumerableOnly) symbols = symbols.filter(function (sym) { return Object.getOwnPropertyDescriptor(object, sym).enumerable; }); keys.push.apply(keys, symbols); } return keys; }

function _objectSpread(target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i] != null ? arguments[i] : {}; if (i % 2) { ownKeys(Object(source), true).forEach(function (key) { _defineProperty(target, key, source[key]); }); } else if (Object.getOwnPropertyDescriptors) { Object.defineProperties(target, Object.getOwnPropertyDescriptors(source)); } else { ownKeys(Object(source)).forEach(function (key) { Object.defineProperty(target, key, Object.getOwnPropertyDescriptor(source, key)); }); } } return target; }

function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

var DEFAULT_STYLES = {
  sidebar: {
    position: "relative"
  },
  content: {
    zIndex: 1,
    transition: "transform 0.2s ease-in-out",
    transform: "translateX(-100%)",
    position: "absolute",
    background: "#ffffff",
    width: "250px",
    height: "100%"
  },
  overlay: {
    pointerEvents: "none",
    transition: "opacity 0.3s ease-in-out",
    position: "absolute",
    background: "rgba(255, 255, 255, 0.8)",
    width: "100%",
    height: "100%",
    opacity: "0"
  }
};
var ACTIVE_STYLES = {
  content: {
    transform: "translateX(0)"
  },
  overlay: {
    pointerEvents: "auto",
    opacity: "1"
  }
};

var Sidebar = function Sidebar(_ref) {
  var isOpen = _ref.isOpen,
      onClose = _ref.onClose,
      content = _ref.content,
      children = _ref.children;
  return /*#__PURE__*/_react["default"].createElement("div", {
    className: "Sidebar",
    style: _objectSpread(_objectSpread({}, DEFAULT_STYLES.sidebar), isOpen ? ACTIVE_STYLES.sidebar : {})
  }, /*#__PURE__*/_react["default"].createElement("div", {
    className: "Sidebar__content",
    style: _objectSpread(_objectSpread({}, DEFAULT_STYLES.content), isOpen ? ACTIVE_STYLES.content : {})
  }, content), /*#__PURE__*/_react["default"].createElement("div", {
    className: "Sidebar__overlay",
    onClick: onClose,
    style: _objectSpread(_objectSpread({}, DEFAULT_STYLES.overlay), isOpen ? ACTIVE_STYLES.overlay : {})
  }), children);
};

var _default = Sidebar;
exports["default"] = _default;
