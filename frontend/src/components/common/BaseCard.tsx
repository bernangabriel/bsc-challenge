import React, { PropsWithChildren } from "react";

interface Props extends PropsWithChildren {
  children: React.ReactNode;
  className?: string | undefined;
}

const BaseCard: React.FC<Props> = ({
  children,
  className = "",
}): React.ReactNode => {
  return <div className={`card ${className}`}>{children}</div>;
};

export default BaseCard;
