import React from "react";

interface Props {
  className?: string | undefined;
  title: string;
  primary?: boolean;
  danger?: boolean;
  onClick?: () => void;
}

const BaseButton: React.FC<Props> = ({
  className,
  title,
  primary = false,
  danger = false,
  onClick,
}): React.ReactElement => {
  const buttonClass = primary
    ? "btn-primary"
    : danger
    ? "btn-danger"
    : "btn-secondary";

  return (
    <button
      type="button"
      className={`btn ${buttonClass} ${className}`}
      onClick={onClick}
    >
      {title}
    </button>
  );
};

export default BaseButton;
