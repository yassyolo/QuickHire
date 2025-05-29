import './AdminDataTable.css';
import { NoDataTd } from './NoDataTd';
import { Th } from './Th';

export interface DataTableProps<T extends { id: number | string }> {
    data: T[];
    columns: (keyof T)[];
    headers: Record<keyof T, string>;
    renderActions?: (row: T) => React.ReactNode;
    noDataTd?: React.ReactNode;
}

export function DataTable<T extends { id: number | string}>({ data, columns, headers, renderActions, noDataTd}: DataTableProps<T>) {
    return (
        <div className="admin-table">
         <table className="table-container" aria-label="data-table">
            <thead aria-label="table-header" key="table-header">
                <tr key="header-row">
                    {columns.map((column) => ( <Th title={headers[column]}></Th> ))}
                    {renderActions &&  <Th title={"Actions"}></Th>}
                </tr>
            </thead>
            <tbody aria-label="table-body" key="table-body">
                {noDataTd ? noDataTd : null}
                {data.length === 0 && noDataTd === null ?  ( <tr> <NoDataTd colSpan={columns.length + 1} /> </tr>) : 
                ( data.map((row) => (
                        <tr key={row.id}>
                            {columns.map((column) => (
                                <td key={String(column)} aria-label={String(column)}> {String(row[column])} </td>
                            ))}
                            {renderActions &&  <td key={`${row.id}-actions`} aria-label="Actions"> {renderActions(row)}</td>}
                        </tr>
                    ))
                )}
            </tbody>
         </table>
        </div>
    );
}
