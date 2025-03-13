export default function Dashboard() {
	return (
		<>
			<div>Dashboard page</div>
			{Array.from({ length: 60 }, (_, key) => (
				<span key={key} >{key}||</span>
			))}
			{Array.from({ length:200 }, (_, key) => (
				<p key={key} >{key}</p>
			))}
		</>
	);
}