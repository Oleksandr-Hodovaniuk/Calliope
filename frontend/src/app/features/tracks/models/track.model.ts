export interface TrackModel {
  id: string,
  name: string,
  author: string | null,
  album: string | null,
  rating: number | null,
  hasFile: boolean,
  createdAt: string
}