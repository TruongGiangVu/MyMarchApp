export default async function ToDoDetailPage({
    params,
}: {
    params: Promise<{ toDoId: string }>
}) {

    const { toDoId } = await params;

    return (
        <div>My toDoId: {toDoId}</div>
    )
}