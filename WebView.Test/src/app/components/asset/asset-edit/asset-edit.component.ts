import { Component, OnInit } from '@angular/core';
import { Asset } from 'src/app/models/asset';
import { ActivatedRoute } from '@angular/router';
import { AssetsService } from 'src/app/services/assets.service';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-asset-edit',
  templateUrl: './asset-edit.component.html',
  styleUrls: ['./asset-edit.component.scss']
})
export class AssetEditComponent implements OnInit {
  public assetData: Asset;
  constructor(private route: ActivatedRoute, private assetsService: AssetsService) { }

  ngOnInit() {
    this.assetsService.asset$.subscribe((data) => {
      this.assetData = data;
      console.log(this.assetData);
    });
    this.route.params.pipe(take(1))
      .subscribe((params: any) => {
        this.assetsService.getAssetById(params.id);
      });
  }

  public handleFormSubmit(): void {
    this.assetsService.UpdateAsset(this.assetData.AssetId, this.assetData).subscribe((data) => {
      alert('The changes updated successully!');
    });
    console.log(this.assetData);
  }
}
