import { Component, inject, OnInit } from '@angular/core';
import { TracksService } from '../../services/track.service';
import { TrackModel } from '../../models/track.model';

@Component({
  imports: [],
  selector: 'app-tracks.component',
  styleUrl: './tracks.component.css',
  templateUrl: './tracks.component.html',
})
export class TracksComponent implements OnInit {
  private readonly tracksService = inject(TracksService);

  tracks: TrackModel[] = [];

  ngOnInit(): void {
    this.loadTracks();
  }

  private loadTracks(): void {
    this.tracksService.getTracks().subscribe({
      next: response => {
        console.log('Tracks response:', response);
        
        this.tracks = response.tracks;
      },
      error: error => {
        console.error('Failed to load tracks:', error);
      }
    });
  }
}
