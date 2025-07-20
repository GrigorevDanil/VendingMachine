export const CoinImage = ({ denomination }: { denomination: number }) => {
  return (
    <div className="col-span-6 flex items-center gap-4">
      <div className="w-20 h-20 relative flex items-center justify-center border-4 border-black rounded-full bg-gray-500 text-white text-4xl opacity-80">
        {denomination}
      </div>
    </div>
  );
};
