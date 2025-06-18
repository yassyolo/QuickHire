import { Conversation } from "./MessageBox";
import axios from "../../../../axiosInstance";



export async function fetchConversationById(id: number): Promise<Conversation> {
  const { data } = await axios.get<Conversation>(`https://localhost:7267/messages/${id}`);
  return data;
}

export async function uploadFile(file: File): Promise<string> {
  const formData = new FormData();
  formData.append("file", file);

  const { data } = await axios.post<{ fileUrl: string }>(
    "https://localhost:7267/files/upload",
    formData,
    { headers: { "Content-Type": "multipart/form-data" } }
  );

  return data.fileUrl;
}

export async function starConversation(id: number): Promise<void> {
  await axios.post(`https://localhost:7267/messages/star/${id}`);
}
