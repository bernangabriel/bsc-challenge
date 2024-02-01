import React from "react";

interface Props {
  title: string;
  value?: string;
  valueClassName?: string;
}

const BaseLabelValue: React.FC<Props> = ({
  title,
  value,
  valueClassName,
}): React.ReactNode => {
  return (
    <div>
      <label className="form-label fw-bold py-0 my-0 fs-6">{title}</label>
      <div className={`fs-8 ${valueClassName ?? ""}`}>{value}</div>
    </div>
  );
};

export default BaseLabelValue;
