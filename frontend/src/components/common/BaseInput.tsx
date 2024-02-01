import React, { HTMLInputTypeAttribute } from "react";

interface Props {
  className?: string | undefined;
  id: string;
  name: string;
  type?: HTMLInputTypeAttribute | undefined;
  label: string;
  value?: string | undefined;
  disabled?: boolean;
  hasError?: boolean | undefined;
  errorMessage?: string | undefined;
  onChange?: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

const BaseInput: React.FC<Props> = ({
  className,
  id,
  name,
  type = "text",
  label,
  value,
  disabled = false,
  hasError = false,
  errorMessage,
  onChange,
}): React.ReactNode => {
  return (
    <div className={className}>
      <label className="form-label fw-bold py-1 my-0 fs-6">{label}</label>
      <input
        style={{
          border: "1px solid #bdc3c7",
        }}
        id={id}
        name={name}
        type={type}
        className="form-control"
        placeholder=""
        value={value}
        onChange={onChange}
        disabled={disabled}
      />
      {hasError && <div className="text-danger">{errorMessage}</div>}
    </div>
  );
};

export default BaseInput;
