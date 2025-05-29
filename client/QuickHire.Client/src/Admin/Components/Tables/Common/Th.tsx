export interface ThProps {
    title: string;
}
export function Th({title} : ThProps) {
    return <th scope="col" aria-label={title} key={title}> {title} </th>
}