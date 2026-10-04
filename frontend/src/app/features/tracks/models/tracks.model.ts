import { TrackModel } from "./track.model";

export interface TracksModel {
  tracks: TrackModel[];
  page: number;
  pageSize: number;
  totalCount: number;
}