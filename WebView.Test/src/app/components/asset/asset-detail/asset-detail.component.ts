import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs/operators';
import { AssetsService } from 'src/app/services/assets.service';
import { Asset } from 'src/app/models/asset';

@Component({
  selector: 'app-asset-detail',
  templateUrl: './asset-detail.component.html',
  styleUrls: ['./asset-detail.component.scss']
})
export class AssetDetailComponent implements OnInit {
  public assetData: Asset;
  constructor(private route: ActivatedRoute, private assetsService: AssetsService) { }

  ngOnInit() {
    this.assetsService.asset$.subscribe((data) => {
      this.assetData = data;
    });
    this.route.params.pipe(take(1))
      .subscribe((params: any) => {
        this.assetsService.getAssetById(params.id);
      });
  }

}
