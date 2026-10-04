import { inject, Service } from '@angular/core';
import { ApiService } from '../../../core/services/api.service';
import { TrackModel } from '../models/track.model';
import { Observable } from 'rxjs';
import { TracksModel } from '../models/tracks.model';

@Service()
export class TracksService {
  private readonly api = inject(ApiService);

  getTrack(id: string): Observable<TrackModel> {
    return this.api.get<TrackModel>(`tracks/${id}`);
  }

  getTracks(page = 1): Observable<TracksModel> {
    return this.api.get<TracksModel>(
      `tracks?page=${page}`
    );
  }

  deleteTrack(id: string): Observable<void> {
    return this.api.delete<void>(
      `tracks/${id}`
    );
  }
}
